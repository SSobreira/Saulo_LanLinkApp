import { Component, ViewChild, Injector, Output, EventEmitter, ElementRef, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { UserServiceProxy, CreateUserDto, RoleDto } from '@shared/service-proxies/service-proxies';
import { UserDepartmentServiceProxy, UserDepartmentDTO, PagedResultDtoOfUserDepartmentDTO } from "shared/service-proxies/service-proxies";
import { DepartmentServiceProxy, DepartmentDTO, PagedResultDtoOfDepartmentDTO } from "shared/service-proxies/service-proxies";
import { AppComponentBase } from '@shared/app-component-base';

import * as _ from "lodash";

@Component({
  selector: 'create-user-modal',
  templateUrl: './create-user.component.html'
})
export class CreateUserComponent extends AppComponentBase implements OnInit {

    @ViewChild('createUserModal') modal: ModalDirective;
    @ViewChild('modalContent') modalContent: ElementRef;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active: boolean = false;
    saving: boolean = false;
    user: CreateUserDto = null;
    roles: RoleDto[] = null;
    departments: DepartmentDTO[] = [];

    constructor(
        injector: Injector,
        private _userService: UserServiceProxy,
        private departmentService: DepartmentServiceProxy,   
        private userdepartmentService: UserDepartmentServiceProxy
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.departmentService.getAll(" ", 0, 100000)
            .subscribe((result) => {
                this.departments = result.items;
            });
        this._userService.getRoles()
        .subscribe((result) => {
            this.roles = result.items;
        });
    }

    show(): void {
        this.active = true;
        this.modal.show();
        this.user = new CreateUserDto();
        this.user.init({ isActive: true });
    }

    onShown(): void {
        $.AdminBSB.input.activate($(this.modalContent.nativeElement));
    }

    save(): void {
        //TODO: Refactor this, don't use jQuery style code
        var roles = [];
        $(this.modalContent.nativeElement).find("[name=role]").each((ind:number, elem:Element) => {
            if($(elem).is(":checked") == true){
                roles.push(elem.getAttribute("value").valueOf());
            }
        });

        var mydepartments = new Array();
        $(this.modalContent.nativeElement).find("[name=department]").each((ind: number, elem: Element) => {
            if ($(elem).is(":checked") == true) {      
                mydepartments.push(elem.getAttribute("value").valueOf());     
            }
        });

        if (mydepartments.length < 1) { this.notify.info(this.l('Add at least one department.')); return;}

        this.user.roleNames = roles;
        this.saving = true;     
       
        this._userService.create(this.user)
            .finally(() => { this.saving = false })
            .subscribe(() => {
                this._userService.getAll(0, 10000)
                    .subscribe((result) => {
                        for (let tuser of result.items) {
                            if (tuser.emailAddress == this.user.emailAddress) { id = tuser.id }
                        }

                        for (let dep of mydepartments) {                               
                            this.userdepartmentService.createOne(id, dep)
                                .finally(() => { this.saving = false; })
                                .subscribe(() => {
                                    this.notify.info(this.l('SavedSuccessfully'));
                                    this.close();
                                    this.modalSave.emit(null);
                                });   
                        }


                    });
            });

        var id = 1;                            

    



      
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
