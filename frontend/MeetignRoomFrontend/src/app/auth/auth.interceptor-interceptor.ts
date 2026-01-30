import { HttpInterceptorFn } from '@angular/common/http';

export const authInterceptorInterceptor: HttpInterceptorFn = (req, next) => {

  console.log('🔐 Interceptor running for:', req.url);

  //  Skip login & register APIs
  if (req.url.includes('/auth/login') || req.url.includes('/auth/register')) {
    console.log(' Skipping auth interceptor for:', req.url);
    return next(req);
  }

  const token = localStorage.getItem('token');
  console.log('🔑 Token found:', token);

  console.log(
    '🔐 Interceptor:',
    req.method,
    req.url,
    'token:',
    token
  );

  if (token) {
    const authReq = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    });

    return next(authReq);
  }

  return next(req);
};
