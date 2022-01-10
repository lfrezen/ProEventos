import { Evento } from './../models/Evento';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';

@Injectable(
  // { providedIn: 'root' } provider pode ser colocado aqui
)
export class EventoService {
  baseUrl = 'https://localhost:5001/api/eventos';

  constructor(private http: HttpClient) { }

  public obterEventos(): Observable<Evento[]> {
    return this
      .http.get<Evento[]>(this.baseUrl)
      .pipe(take(1));
  }

  public obterEventosPorTema(tema: string): Observable<Evento[]> {
    return this.http
      .get<Evento[]>(`${this.baseUrl}/tema/${tema}`)
      .pipe(take(1));
  }

  public obterEventoPorId(id: number): Observable<Evento> {
    return this.http
      .get<Evento>(`${this.baseUrl}/${id}`)
      .pipe(take(1));
  }

  public post(evento: Evento): Observable<Evento> {
    return this.http
      .post<Evento>(this.baseUrl, evento)
      .pipe(take(1));
  }

  public put(evento: Evento): Observable<Evento> {
    return this.http
      .put<Evento>(`${this.baseUrl}/${evento.id}`, evento)
      .pipe(take(1));
  }

  public deleteEvento(id: number): Observable<any> {
    return this.http
      .delete(`${this.baseUrl}/${id}`)
      .pipe(take(1));
  }
}
