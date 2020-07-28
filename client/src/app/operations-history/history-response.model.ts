import { HistoryOperation } from './history-operation.model';
export interface HistoryResponse{
    operationList: HistoryOperation[];
    operationCount: number;
    filter: string;
}