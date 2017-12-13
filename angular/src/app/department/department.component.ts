import { Component, Injector, ViewChild } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto } from "shared/paged-listing-component-base";
import { DepartmentServiceProxy, DepartmentDTO, PagedResultDtoOfDepartmentDTO } from "shared/service-proxies/service-proxies";    
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { CreateDepartmentComponent } from "app/department/create-department/create-department.component";
import { CreateRoleComponent } from "app/roles/create-role/create-role.component";
import { EditRoleComponent } from "app/roles/edit-role/edit-role.component";
//import { EditRoleComponent } from "app/roles/edit-role/edit-role.component";

@Component({
    templateUrl: './department.component.html',
    animations: [appModuleAnimation()]
})
export class DepartmentComponent extends PagedListingComponentBase<DepartmentDTO> {
                                                                                              
    @ViewChild('createDepartmentModal') CreateDepartmentComponent: CreateDepartmentComponent;
    //@ViewChild('editRoleModal') editRoleModal: EditRoleComponent;

    departments: DepartmentDTO[] = [];

    constructor(
        private injector: Injector,
        private departmentService: DepartmentServiceProxy
    ) {
        super(injector);
    }

    list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
        this.departmentService.getAll(" ", request.skipCount, request.maxResultCount       )
            .finally(() => {
                finishedCallback();
            })
            .subscribe((result: PagedResultDtoOfDepartmentDTO) => {
                this.departments = result.items;
                this.showPaging(result, pageNumber);
            });
    }

    delete(department: DepartmentDTO): void {
        //this.departmentService.delete(department.id)
        //abp.notify.info("Deleting Department: " + department.name);
        //try {
        //    this.departmentService.delete(department.id).finally(() => {
        //        abp.notify.info("Deleted Department: " + department.name);
        //        this.refresh();
        //    })
        //}
        //catch (e) {    
        //      abp.notify.info("Deleted Department: " + e.message);   
        //}      
        
        abp.message.confirm(
            "Remove Users from this Department and delete '" + department.name + " ' ?",
            "Permanently delete this Department",
            (result: boolean)  => {                          
                if (result == true) {
                    abp.notify.info("Deletando...");
                    this.departmentService.delete(department.id)
                        .finally(() => {
                            abp.notify.info("Deleted Department: " + department.name);
                            this.refresh();
                        })
                        .subscribe(() => { });
                } else { abp.notify.info("Cancelado.");}
            }
        );
    }

    // Show Modals
    createDepartment(): void {            
        try {    
            this.CreateDepartmentComponent.show();
        } catch(e){
            abp.notify.info(e);
        }    
    }

    //editDepartment(role: DepartmentDto): void {
    //    this.editRoleModal.show(role.id);
    //}
}
