import { HistoryOperation } from './history-operation.model';
export interface HistoryResponse{
    operationsList: HistoryOperation[];
    OperationsTotal: number;
    filter: string;
}