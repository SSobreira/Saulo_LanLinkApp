import { Component, Injector, ViewChild } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto } from "shared/paged-listing-component-base";
import { DepartmentServiceProxy, DepartmentDTO, PagedResultDtoOfDepartmentDTO } from "shared/service-proxies/service-proxies";
import { TransactionServiceProxy, TransactionDTO, PagedResultDtoOfTransactionDTO } from "shared/service-proxies/service-proxies";
import { UserServiceProxy, UserDto, PagedResultDtoOfUserDto } from '@shared/service-proxies/service-proxies';  
import { appModuleAnimation } from '@shared/animations/routerTransition';        
import { CreateTransactionComponent } from 'app/transaction/create-transaction/create-transaction.component';

@Component({
    templateUrl: './transaction.component.html',
    animations: [appModuleAnimation()]
})

     

export class TransactionComponent extends PagedListingComponentBase<TransactionDTO> {
     

    @ViewChild('createTransactionModal') createTransactionModal: CreateTransactionComponent;

    transactions: TransactionDTO[] = [];
    departments: DepartmentDTO[] = [];
    users: UserDto[] = [];

    constructor(
        private injector: Injector,
        private transactionService: TransactionServiceProxy,
        private userService: UserServiceProxy,  
        private departmentService: DepartmentServiceProxy
    ) {
        super(injector);
    }

    list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
        this.departmentService.getAll(" ", 0, 10000)
                                            .finally(() => {
                                                finishedCallback();
                                                this.userService.getAll(0, 100000)
                                                    .finally(() => {
                                                        finishedCallback();
                                                        this.transactionService.getAll(" ", request.skipCount, request.maxResultCount)
                                                            .finally(() => {
                                                                finishedCallback();
                                                            })
                                                            .subscribe((result: PagedResultDtoOfTransactionDTO) => {
                                                                this.transactions = result.items;
                                                                for (let entry of this.transactions) {
                                                                    for (let dep of this.departments) {
                                                                        if (dep.id == entry.departmentCreatorId) { entry.departmentCreator = dep; }
                                                                    }
                                                                    for (let user of this.users) {
                                                                        if (user.id == entry.userCreatorId) { entry.userCreator = user; }
                                                                    }
                                                                }
                                                                this.showPaging(result, pageNumber);
                                                            });
                                                    })
                                                    .subscribe((result: PagedResultDtoOfUserDto) => {
                                                        this.users = result.items;
                                                    });
                                            })
                                            .subscribe((result: PagedResultDtoOfDepartmentDTO) => {
                                                this.departments = result.items;         
                                        });
     
       
    }

    delete(transaction: TransactionDTO): void {
        abp.message.confirm(
            "Remove Users from this Transaction and delete '" + transaction.description + " ' ?",
            "Permanently delete this Transaction",
            (result: boolean)  => {                          
                if (result == true) {
                    abp.notify.info("Deletando...");
                    this.transactionService.delete(transaction.id)
                        .finally(() => {
                            abp.notify.info("Deleted Transaction: " + transaction.description);
                            this.refresh();
                        })
                        .subscribe(() => { });
                } else { abp.notify.info("Cancelado.");}
            }
        );
    }

     //Show Modals
     createTransaction(): void {             
         try {
             this.createTransactionModal.show();
         } catch (e) {
             abp.notify.info(e);
         }    
    }

    //editTransaction(role: TransactionDto): void {
    //    this.editRoleModal.show(role.id);
    //}
}
