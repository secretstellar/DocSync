import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IAPIResponseModel, IDocument, IDocumentInfoModel, IUser } from '../models/interfaces';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor() { }

  http = inject(HttpClient);

  getAllDocumentInfo(): Observable<IAPIResponseModel> {
    return this.http.get<IAPIResponseModel>(environment.API_URL + "DocumentInfo")
  }

  addDocumentInfo(docInfoObj: IDocumentInfoModel): Observable<IAPIResponseModel> {
    return this.http.post<IAPIResponseModel>(environment.API_URL + "DocumentInfo", docInfoObj)
  }

  uploadDocument(formData: FormData): Observable<IAPIResponseModel> {
    return this.http.post<IAPIResponseModel>(environment.API_URL + "Document/upload", formData)
  }


}
