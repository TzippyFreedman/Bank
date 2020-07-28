import { HistoryOperation } from './history-operation.model';
export interface HistoryResponse {
    operationsList: HistoryOperation[];
    operationsTotal: number;
    filter: string;
}