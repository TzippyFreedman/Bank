export interface HistoryOperation {
    operationTime: Date;
    balance: number;
    isCredit: boolean;
    transactionId: string;
	transactionAmount :number;
}