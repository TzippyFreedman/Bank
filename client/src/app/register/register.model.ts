import { UserAddress } from './userAddress.model';

export interface Register {
    verificationCode: string;
    firstName: string;
    lastName: string;
    email: string;
    password: string;
    address:UserAddress;
}