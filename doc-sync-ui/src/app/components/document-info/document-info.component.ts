import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../../services/api.service';
import { IAPIResponseModel, IDocumentInfoModel } from '../../models/interfaces';
import { CommonModule } from '@angular/common';
import { UserStoreService } from '../../services/user-store.service';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-document-info',
  standalone: true,
  imports: [CommonModule,],
  templateUrl: './document-info.component.html',
  styleUrl: './document-info.component.css'
})
export class DocumentInfoComponent implements OnInit {
  documentInfoList: any[] = [];
  selectedFile: File | null = null;
  currentPage: number = 1;
  pageSize: number = 5;
  isLoading: boolean = true;
  documentInfo: IDocumentInfoModel;
  role: string = "";
  documentId: string = '';

  authService = inject(AuthService);
  userStore = inject(UserStoreService);

  constructor() {
    this.documentInfo = {
      id: 0,
      documentName: '',
      statusId: 0,
      createdBy: '',
      updatedBy: undefined,
    };
  }

  router = inject(Router);
  apiService = inject(ApiService);

  ngOnInit(): void {
    this.userStore.getRoleFromStore().subscribe((res: any) => {
      let roleFromToken = this.authService.getRoleFromToken();
      this.role = res.toLowerCase() || roleFromToken.toLowerCase();
    });

    if (this.role === 'admin' || this.role === 'manager')
      this.loadAllDocumentInfo();
    else
      this.isLoading = false;
  }

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
  }

  onUpload() {
    if (!this.selectedFile) {
      alert('Please select a file');
      return;
    }
    this.isLoading = true;

    this.documentInfo.documentName = this.selectedFile.name;
    this.documentInfo.createdBy = this.authService.getUserName()!;
    // Status Id  = 1 for In Progress
    this.documentInfo.statusId = 1;

    this.apiService.addDocumentInfo(this.documentInfo).subscribe((res: IAPIResponseModel) => {

      this.documentId = res?.data;
      if (res.isSuccess) {
        // Upload document to message queue
        if (this.uploadDocument())
          alert("Document uploaded successfully");
        else
          alert("Oops! Document upload failed.");
      }
      else
        alert("Oops! Document upload failed.");
    }, error => {
      console.log(error);
    }, () => {
      this.selectedFile = null;
      this.loadAllDocumentInfo();
    });
  }

  loadAllDocumentInfo() {
    return this.apiService.getAllDocumentInfo().subscribe((res: IAPIResponseModel) => {
      this.documentInfoList = res.data;
      this.isLoading = false;
    }, error => {
      console.log(error);
      this.isLoading = false;
    })
  }

  uploadDocument() {
    const formData = new FormData();
    formData.append('documentId', this.documentId);
    formData.append('document', this.selectedFile!, this.selectedFile!.name);

    return this.apiService.uploadDocument(formData).subscribe(res => {
      return res.isSuccess;
    }, error => {
      console.log(error);
      this.isLoading = false;
      return false;
    });
  }

  onRefresh() {
    this.loadAllDocumentInfo();
  }
}
