import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AuthService {

  private api = 'http://localhost:8000/api';

  constructor(private http: HttpClient ) {}

  login(email: string, password: string): Observable<any>  {
    return this.http.post<any>(`${this.api}/login`, { email, password }).pipe(
      tap(res => {
        this.saveToken(res.token);
        this.saveUserId(res.id); 
      })
    );
  }

  saveToken(token: string) {
    localStorage.setItem('token', token);
  }

  getToken() {
    return localStorage.getItem('token');
  }

  saveUserId(id: number) {
    localStorage.setItem('user_id', id.toString());
  }

  getUserId(): number | null {
    const id = localStorage.getItem('user_id');
    return id ? +id : null;
  }

  isAuthenticated(): boolean {
    return !!this.getToken();
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user_id');
  }
}
