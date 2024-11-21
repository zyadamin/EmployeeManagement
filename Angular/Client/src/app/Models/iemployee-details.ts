import { IProject } from "./iproject";

export interface IEmployeeDetails {
    id:number,
    name:string,
    email:string,
    projects:IProject[]
}
