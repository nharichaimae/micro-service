import { Injectable } from '@angular/core';
import { HttpClient,HttpHeaders  } from '@angular/common/http';
import { AuthService } from './auth';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class Piece {
  private api = 'http://localhost:5182';

  constructor(private http: HttpClient ,private auth: AuthService) {}

 addPiece(name: string ) {
    const token = this.auth.getToken();
    if (!token) throw new Error('Utilisateur non authentifié');

   const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`, 
      'Content-Type': 'application/json'
    });

    return this.http.post(`${this.api}/api/Pieces/Add`, { name }, { headers :{ 'X-AUTH-TOKEN': token } });

}
getPiecesWithEquipements(user_id: number): Observable<any[]> {
  const token = this.auth.getToken();  
  if (!token) throw new Error('Utilisateur non authentifié');

  const headers = new HttpHeaders({
      'X-AUTH-TOKEN': token, 
  });

  return this.http.get<any[]>(`${this.api}/api/Pieces/with-equipements/${user_id}`, { headers });
}


}








   

