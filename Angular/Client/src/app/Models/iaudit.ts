export interface IAudit {
    action:string,
    name:string,
    timeStamp:string,
    oldData?:string,
    newData?:string
}
