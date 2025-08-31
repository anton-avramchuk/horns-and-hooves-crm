import { inject, Injectable } from '@angular/core';
import {
  IAuthService,
  IClaimsPermissionService,
  ILoginResponse,
  IRolePermissionService,
  IUserInformationProvider,
} from './interfaces';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { AUTH_ENDPOINT } from '../crm-core-auth.module';
import { HttpClient } from '@angular/common/http';
import { UserFullNameProvider, UserNameProvider } from '../providers';
import { TokenProvider } from '../providers/token-provider.service';
import { AuthNavigationService } from './auth.navigation.service';
import { jwtDecode } from 'jwt-decode';
import { LocalStorageService } from '@horns-and-hooves/core';

const rolesKey = 'roles';
const claimsKey = 'claims';
@Injectable()
export class TokenAuthService
  implements
    IAuthService,
    IRolePermissionService,
    IClaimsPermissionService,
    IUserInformationProvider
{
  private isAuth: BehaviorSubject<boolean> = new BehaviorSubject(false);

  private storageService: LocalStorageService = inject(LocalStorageService);

  private loginPath: string = inject(AUTH_ENDPOINT);

  private httpClient: HttpClient = inject(HttpClient);

  private userNameProvider: UserNameProvider = inject(UserNameProvider);

  private userFullNameProvider: UserFullNameProvider =
    inject(UserFullNameProvider);

  private tokenProvider: TokenProvider = inject(TokenProvider);

  private navigationService: AuthNavigationService = inject(
    AuthNavigationService
  );

  constructor() {
    this.isAuth.next(this.isAuthenticated());
  }
  login<TResponse extends ILoginResponse>(
    userName: string,
    password: string
  ): Observable<TResponse> {
    return this.httpClient
      .post<TResponse>(this.loginPath, { userName, password })
      .pipe(
        tap((x) => {
          this.setRoles(x.roles);
          this.setClaims(x.claims);
          this.setToken(x.token);
          this.userFullNameProvider.setName(x.userFullName);
          this.userNameProvider.setUserName(x.userName);
        })
      );
  }
  hasRoles(roles: string[]): boolean {
    const userRoles = this.getRoles();
    return roles.some((claim) => userRoles.includes(claim));
  }
  hasClaims(claims: string[]): boolean {
    const savedClaims = this.getClaims();
    return claims.some((claim) => savedClaims.includes(claim));
  }

  isAuthenticated(): boolean {
    return this.tokenProvider.getToken() !== null;
  }

  private setToken(token: string): void {
    this.tokenProvider.setToken(token);
    this.isAuth.next(this.isAuthenticated());
  }

  logOff(): void {
    this.tokenProvider.setToken('');
    this.storageService.remove(rolesKey);
    this.storageService.remove(claimsKey);
    this.userFullNameProvider.setName('');
    this.userNameProvider.setUserName('');
    this.isAuth.next(false);
    this.navigationService.navigateToLogin();
  }

  private setRoles(roles: string[]): void {
    this.storageService.set(rolesKey, roles);
  }

  getRoles(): string[] {
    const result: string[] = this.storageService.get(rolesKey) || [];
    return result;
  }

  private setClaims(claims: string[]): void {
    this.storageService.set(claimsKey, claims);
  }

  getClaims(): string[] {
    const result: string[] = this.storageService.get(claimsKey) || [];
    return result;
  }

  currnetUserId(): string {
    const token = jwtDecode(this.tokenProvider.getToken()) as any;

    return token.nameid;
  }
}
