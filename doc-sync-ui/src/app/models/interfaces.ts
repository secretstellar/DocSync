export interface IAPIResponseModel {
    message: string,
    isSuccess: boolean,
    data: any
}

export interface IDocumentInfoModel {
    id: number
    documentName: string,
    status?: string,
    statusId: number
    createdBy?: string,
    // createdDate?: string,
    updatedBy?: string,
    // updatedDate?: string
}

export interface IDocument {
    documentId: number
    document: File,
}

export interface IUser {
    userName: string,
    password: string,
}

export interface IUserDetails {
    name: string,
    email: string,
    role: string,
    userName: string,
    password: string
}