import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Place {
  id: number;
  name: string;
  postalCode: number;
  population: number;
}

@Injectable({
  providedIn: 'root'
})
export class PlaceService {
  private apiUrl = 'https://localhost:7007/api/places'; 

  constructor(private http: HttpClient) { }

  getPlaces(): Observable<Place[]> {
    return this.http.get<Place[]>(this.apiUrl);
  }

  getPlace(id: number): Observable<Place> {
    return this.http.get<Place>(`${this.apiUrl}/${id}`);
  }

  addPlace(place: Place): Observable<Place> {
    return this.http.post<Place>(this.apiUrl, place);
  }

  updatePlace(id: number, place: Place): Observable<Place> {
    return this.http.put<Place>(`${this.apiUrl}/${id}`, place);
  }

  deletePlace(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  getPlaceStats(id: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/${id}/stats`);
  }
}
