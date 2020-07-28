export interface HistoryOperation {
    OperationTime: Date;
    Amount: number;
    Balance: number;
    IsDebit: boolean;
    TransactionId: string;
}