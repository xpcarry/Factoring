import { Bank } from "./bank";
import { ContractStatus } from "./contract-status.enum";
import { Invoice } from "./invoice";

export interface Contract {
    contractId: string;
    issueDate: Date;
    amount: number;
    debtorName: string;
    status: ContractStatus;
    bankName: string;
    bankSWIFT: string;
    invoices: Invoice[];
  }