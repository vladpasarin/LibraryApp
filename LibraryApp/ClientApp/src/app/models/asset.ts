import { Status } from './status'

export interface Asset {
    id: number;
    status: Status;
    cost: number;
    imageUrl: string;
    type: string;
}