import { Routes } from '@angular/router';
import { ContractListComponent } from './contracts/contract-list/contract-list.component';

export const routes: Routes = [
    { 
        path: '', 
        component: ContractListComponent,
        title: 'Factoring Contracts'
      }
];
