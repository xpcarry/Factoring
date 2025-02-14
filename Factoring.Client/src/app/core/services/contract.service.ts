import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Contract } from '../models/contract';
import { ContractStatus } from '../models/contract-status.enum';

@Injectable({
  providedIn: 'root'
})
export class ContractService {
  private apiUrl = 'http://localhost:5030/api/Contracts';

  constructor(private http: HttpClient) { }

  getContracts(status?: ContractStatus): Observable<Contract[]> {
    let params = new HttpParams();
    if (status) {
      params = params.append('status', status);
    }
    return this.http.get<Contract[]>(this.apiUrl, { params });
  }
}