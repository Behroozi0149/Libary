export interface AuthOkResponse{
    tokenType:string;
    accessToken:string;
    expiresIn:number;
    refreshToken:string;
}