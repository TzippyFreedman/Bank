export interface HistoryOperation {
    OperationTime: Date;
    Balance: number;
    IsDebit: boolean;
    TransactionId: string;
	TransactionAmount :number;
}