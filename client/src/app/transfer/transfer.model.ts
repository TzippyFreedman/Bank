import { Guid } from "guid-typescript";

export interface Transfer{
    amount: number,
    srcAccount: string,
    destAccount : string
}