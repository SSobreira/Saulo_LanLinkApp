import { Component, ViewChild, Injector, Output, EventEmitter, ElementRef, OnInit } from '@angular/core';
import { DepartmentServiceProxy, DepartmentDTO, PagedResultDtoOfDepartmentDTO } from "shared/service-proxies/service-proxies";   
import { ModalDirective } from 'ngx-bootstrap';       
import { AppComponentBase } from '@shared/app-component-base';

@Component({
    selector: 'create-department-modal',
    templateUrl: './create-department.component.html'
})
export class CreateDepartmentComponent extends AppComponentBase implements OnInit {
    @ViewChild('createDepartmentModal') modal: ModalDirective;
    @ViewChild('modalContent') modalContent: ElementRef;

    active: boolean = false;
    saving: boolean = false;
                                                        
    department: DepartmentDTO = null;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    constructor(
        injector: Injector,
        private _DepartmentService: DepartmentServiceProxy
    ) {
        super(injector);
    }

    ngOnInit(): void {
       
    }

    show(): void {
        this.active = true;
        this.department = new DepartmentDTO();
        this.department.init({ isStatic: false });

        this.modal.show();
    }

    onShown(): void {
        $.AdminBSB.input.activate($(this.modalContent.nativeElement));
    }

    save(): void {
        
                                               

        this.saving = true;
        this._DepartmentService.createOne(this.department.name)
            .finally(() => { this.saving = false; })
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
            });
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
