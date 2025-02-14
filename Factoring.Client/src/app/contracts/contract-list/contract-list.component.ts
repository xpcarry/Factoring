import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { CommonModule, CurrencyPipe } from '@angular/common';
import { MatTableModule, MatTableDataSource } from '@angular/material/table';
import { MatSelectModule } from '@angular/material/select';
import { MatChipsModule } from '@angular/material/chips';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatOptionModule } from '@angular/material/core';
import { FormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectChange } from '@angular/material/select';
import { ShortenIdPipe } from '../../core/pipes/shorten-id.pipe';
import { Contract } from '../../core/models/contract';
import { ContractStatus } from '../../core/models/contract-status.enum';
import { ContractService } from '../../core/services/contract.service';
import { trigger, state, style, transition, animate } from '@angular/animations';

@Component({
  selector: 'app-contract-list',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatSelectModule,
    MatChipsModule,
    MatFormFieldModule,
    MatInputModule,
    MatOptionModule,
    FormsModule,
    CurrencyPipe,
    ShortenIdPipe,
    MatIconModule
  ],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)'))
    ])
  ],
  templateUrl: './contract-list.component.html',
  styleUrls: ['./contract-list.component.scss']
})
export class ContractListComponent implements OnInit {
  dataSource = new MatTableDataSource<Contract>();
  displayedColumns: string[] = ['expand', 'contractId', 'debtorName', 'bank', 'amount', 'status'];
  statuses = Object.values(ContractStatus);
  selectedStatus: ContractStatus | 'all' = 'all';
  expandedElement: Contract | null = null;

  constructor(private contractService: ContractService, private cdr: ChangeDetectorRef) {}

  ngOnInit(): void {
    console.log('Component initialized');
    this.loadContracts();
  }

  loadContracts(): void {
    const status = this.selectedStatus !== 'all' ? this.selectedStatus : undefined;
    this.contractService.getContracts(status).subscribe({
      next: (contracts) => {
        this.dataSource.data = contracts;
        console.log('Contracts loaded:', contracts);
      },
      error: (err) => {
        console.error('Error loading contracts:', err);
      }
    });
  }

  onStatusChange(event: MatSelectChange): void {
    this.selectedStatus = event.value;
    this.loadContracts();
  }

  toggleRow(element: Contract): void {
    this.expandedElement =
      this.expandedElement && this.expandedElement.contractId === element.contractId ? null : element;
    this.cdr.detectChanges();
  }

  getStatusColor(status: ContractStatus): string {
    return {
      [ContractStatus.Active.toString()]: 'primary',
      [ContractStatus.Completed.toString()]: 'accent',
      [ContractStatus.Terminated.toString()]: 'warn'
    }[status];
  }

  isExpanded = (index: number, element: Contract): boolean => {
    return this.expandedElement ? this.expandedElement.contractId === element.contractId : false;
  }
}
