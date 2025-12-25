import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth'; // ton service AuthService

@Injectable({
  providedIn: 'root'
})
export class EquipementService {

  private apiUrl = 'http://localhost:5182/api/Equipement/Add';

  constructor(private http: HttpClient, private auth: AuthService) {}

  addEquipement(equipement: any) {
    const token = this.auth.getToken(); 
    if (!token) throw new Error('Utilisateur non authentifié');

    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'X-AUTH-TOKEN': token 
    });

    return this.http.post(this.apiUrl, equipement, { headers });
  }
}
