import { IProjectRequest } from "./iproject-request";

export interface IEmployeeRequest {
    name:string,
    email:string,
    projects: IProjectRequest[]
}
