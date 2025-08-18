import {
  InjectionToken,
  ModuleWithProviders,
  NgModule,
  Optional,
  Provider,
  SkipSelf,
  Type,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  IAuthService,
  IClaimsPermissionService,
  IRolePermissionService,
  IUserInformationProvider,
} from './services';
import { AuthGuard } from './guards/auth.guard';
import { AuthRolesGuard } from './guards/authRoles.guard';
import { AuthClaimsGuard } from './guards/authClaims.guard';
import { Routes } from '@angular/router';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from './interceptors/AuthInterceptor';
import { throwIfAlreadyLoaded } from '@horns-and-hooves/core';
import { TokenAuthService } from './services/token.auth.service';
export const AUTH_SERVICE = new InjectionToken<IAuthService>('crmAuthService');
export const ROLE_PERMISSION_SERVICE =
  new InjectionToken<IRolePermissionService>('crmRolePermissionService');
export const USER_INFO_PROVIDER = new InjectionToken<IUserInformationProvider>(
  'crmUserInformationProvider'
);
export const CLAIMS_PERMISSION_SERVICE =
  new InjectionToken<IClaimsPermissionService>('crmClaimsPermissionService');
export const AUTH_LOGIN_PATH = new InjectionToken<string>('crmAuthLoginPath');
export const AUTH_ENDPOINT = new InjectionToken<string>('AUTH_ENDPOINT');
export const AUTH_ROUTES = new InjectionToken<Routes>('crmAuthRoutes');
export interface IAuthModuleConfig {
  authService?: Type<any>;
  claimsPermissionService?: Type<any>;
  rolePermissionService?: Type<any>;
  userInformationProvider?: Type<any>;
  settings?: {
    prefix?: string;
    layout?: Type<any>;
    login?: {
      endPoint?: string;
      path?: string;
      component?: Type<any>;
    };
    fogot?: {
      component: Type<any>;
      path?: string;
    };
  };
}

@NgModule({
  imports: [CommonModule],
  providers: [],
})
export class HornsAndHoovesCoreAuthModule {
  constructor(
    @Optional() @SkipSelf() parentModule: HornsAndHoovesCoreAuthModule
  ) {
    throwIfAlreadyLoaded(parentModule, 'HornsAndHoovesCoreAuthModule');
  }
  static forRoot(
    config: IAuthModuleConfig
  ): ModuleWithProviders<HornsAndHoovesCoreAuthModule> {
    let loginPath: string;

    if (config.settings?.prefix && config.settings?.login?.path) {
      loginPath = config.settings?.prefix + '/' + config.settings?.login?.path;
    } else if (config.settings?.login?.path) {
      loginPath = 'auth/' + config.settings?.login?.path;
    } else {
      loginPath = 'auth/login';
    }

    const routes: Routes = [
      {
        path: config.settings?.prefix ?? '',
        component: config.settings?.layout,
        children: [
          {
            path: config.settings?.login?.path,
            component: config.settings?.login?.component,
          },
          {
            path: config.settings?.fogot?.path,
            component: config.settings?.fogot?.component,
          },
        ],
      },
    ];

    const providers: Provider[] = [
      AuthGuard,
      AuthRolesGuard,
      AuthClaimsGuard,
      {
        provide: HTTP_INTERCEPTORS,
        useClass: AuthInterceptor,
        multi: true,
      },
      {
        provide: AUTH_SERVICE,
        useClass: config.authService ?? TokenAuthService,
      },
      {
        provide: ROLE_PERMISSION_SERVICE,
        useClass: config.rolePermissionService ?? TokenAuthService,
      },
      {
        provide: CLAIMS_PERMISSION_SERVICE,
        useClass: config.claimsPermissionService ?? TokenAuthService,
      },
      { provide: AUTH_LOGIN_PATH, useValue: loginPath },
      { provide: AUTH_ROUTES, useValue: routes },
      {
        provide: AUTH_ENDPOINT,
        useValue: config.settings?.login?.endPoint ?? 'auth/login',
      },
      {
        provide: USER_INFO_PROVIDER,
        useClass: config.userInformationProvider ?? TokenAuthService,
      },
    ];
    return {
      ngModule: HornsAndHoovesCoreAuthModule,
      providers: [providers],
    };
  }
}
