import { Component, ViewChild, Injector, Output, EventEmitter, ElementRef, OnInit } from '@angular/core';                             
import { TransactionServiceProxy, TransactionDTO, PagedResultDtoOfTransactionDTO } from "shared/service-proxies/service-proxies";
import { DepartmentServiceProxy, DepartmentDTO, PagedResultDtoOfDepartmentDTO } from "shared/service-proxies/service-proxies";  
import { ModalDirective } from 'ngx-bootstrap';       
import { AppComponentBase } from '@shared/app-component-base';

@Component({
    selector: 'create-transaction-modal',
    templateUrl: './create-transaction.component.html'
})
export class CreateTransactionComponent extends AppComponentBase implements OnInit {
    @ViewChild('createTransactionModal') modal: ModalDirective;
    @ViewChild('modalContent') modalContent: ElementRef;

    active: boolean = false;
    saving: boolean = false;
                                                        
    transaction: TransactionDTO = null;
    departments: DepartmentDTO[] = [];

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    constructor(
        injector: Injector,
        private _TransactionService: TransactionServiceProxy,
        private departmentService: DepartmentServiceProxy
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.departmentService.getAll(" ", 0, 10000)
            .finally(() => {              
            })
            .subscribe((result: PagedResultDtoOfDepartmentDTO) => {
                this.departments = result.items;
            });
    }

    show(): void {
        this.active = true;
        this.transaction = new TransactionDTO();
        this.transaction.init({ isStatic: false });

        this.modal.show();
    }

    onShown(): void {
        $.AdminBSB.input.activate($(this.modalContent.nativeElement));
    }

    save(): void {                              

        this.saving = true;
        try {
            this._TransactionService.createOne(1, this.transaction.description, this.transaction.value, this.transaction.departmentCreatorId)
                .finally(() => { this.saving = false; })
                .subscribe(() => {
                    this.close();
                    this.modalSave.emit(null);
                });
        } catch (e) {
            this.notify.info(e);

        }
       
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
