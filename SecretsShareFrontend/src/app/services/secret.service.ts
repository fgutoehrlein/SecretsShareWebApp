import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { SecretDataModel } from '../models/secret-data-model';

@Injectable({
  providedIn: 'root'
})
export class SecretService {
  apiUrl: string = 'http://localhost:8001';

  postHttpOptions = {
    headers: new HttpHeaders({'Accept': 'text/plain', 'Content-Type': 'text/plain'}),
    responseType: 'text'
  };

  constructor(private http: HttpClient) { }

  public getSecret(secretId: string, password: string): Observable<any> {
    let headers = new HttpHeaders({
      'Content-Type': 'text/plain',
      'Guid-Key': secretId,
      'Password' : password
    });

    return this.http.get(this.apiUrl + '/Secret', {headers: headers, responseType:'text'})
      .pipe(retry(1), catchError(this.handleError));
  }

  public postSecret(secretData: SecretDataModel): Observable<any> {
    let headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });

    return this.http.post(this.apiUrl + '/Secret', JSON.stringify(secretData), {headers: headers, responseType:'text'})
      .pipe(retry(1), catchError(this.handleError));
  }

  handleError(error: any) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // handle client-side error
      errorMessage = error.error.message;
    } else {
      // handle server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    console.error(errorMessage);
    return throwError(() => {
      return errorMessage;
    });
  }
}
