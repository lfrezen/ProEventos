import { Evento } from './../models/Evento';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable(
  // { providedIn: 'root' } provider pode ser colocado aqui
)
export class EventoService {
  baseUrl = 'https://localhost:5001/api/eventos';

  constructor(private http: HttpClient) { }

  public obterEventos(): Observable<Evento[]> {
    return this.http.get<Evento[]>(this.baseUrl);
  }

  public obterEventosPorTema(tema: string): Observable<Evento[]> {
    return this.http.get<Evento[]>(`${this.baseUrl}/tema/${tema}`);
  }

  public obterEventoPorId(id: number): Observable<Evento[]> {
    return this.http.get<Evento[]>(`${this.baseUrl}/${id}`);
  }
}
