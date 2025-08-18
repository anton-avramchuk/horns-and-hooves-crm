import { Observable } from "rxjs";

export interface IAuthService {
    login<TResponse extends ILoginResponse>(userName: string, password: string): Observable<TResponse>;
    isAuthenticated(): boolean;
    logOff(): void;
}

export interface IRolePermissionService {
    hasRoles(roles: string[]): boolean;
}

export interface IClaimsPermissionService {
    hasClaims(claims: string[]): boolean;
}


export interface IUserInformationProvider{
    currnetUserId(): string 
}


export interface ILoginResponse {
    token: string;
    userName: string;
    roles: string[];
    claims: string[];
    userFullName: string;
}