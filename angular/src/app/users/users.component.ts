import { Component, Injector, ViewChild } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { UserServiceProxy, UserDto, PagedResultDtoOfUserDto } from '@shared/service-proxies/service-proxies';
import { DepartmentServiceProxy, DepartmentDTO, PagedResultDtoOfDepartmentDTO } from "shared/service-proxies/service-proxies";
import { UserDepartmentServiceProxy, UserDepartmentDTO, PagedResultDtoOfUserDepartmentDTO } from "shared/service-proxies/service-proxies";
import { PagedListingComponentBase, PagedRequestDto } from "shared/paged-listing-component-base";
import { CreateUserComponent } from "app/users/create-user/create-user.component";
import { EditUserComponent } from "app/users/edit-user/edit-user.component";

@Component({
    templateUrl: './users.component.html',
    animations: [appModuleAnimation()]
})
export class UsersComponent extends PagedListingComponentBase<UserDto> {

    @ViewChild('createUserModal') createUserModal: CreateUserComponent;
    @ViewChild('editUserModal') editUserModal: EditUserComponent;

    active: boolean = false;
    users: UserDto[] = [];
    departments: DepartmentDTO[] = [];
    userdepartments: UserDepartmentDTO[] = [];

    constructor(
        injector: Injector,
        private _userService: UserServiceProxy,
        private userdepartmentService: UserDepartmentServiceProxy,
        private departmentService: DepartmentServiceProxy
    ) {
        super(injector);
    }

    protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
        this.departmentService.getAll(" ",0,100000)
            .finally(() => {
                finishedCallback();
                this.userdepartmentService.getAll(" ", 0, 100000)
                    .finally(() => {
                        finishedCallback();
                        this._userService.getAll(request.skipCount, request.maxResultCount)
                            .finally(() => {
                                finishedCallback();
                            })
                            .subscribe((result: PagedResultDtoOfUserDto) => {
                                this.users = result.items;
                                for (let entry of this.users) {
                                    entry.departments = new Array<DepartmentDTO>();
                                    for (let ud of this.userdepartments) {
                                        if (ud.refUserID == entry.id) {
                                            for (let dep of this.departments) {
                                                if (ud.refDepartmentID == dep.id) { entry.departments.push(dep);}
                                            }
                                            
                                        }
                                    }
                                    
                                }
                                this.showPaging(result, pageNumber);
                            });
                    })
                    .subscribe((result: PagedResultDtoOfUserDepartmentDTO) => {
                        this.userdepartments = result.items;
                    });
            })
            .subscribe((result: PagedResultDtoOfDepartmentDTO) => {
                this.departments = result.items;       
            });
       
    }

    protected delete(user: UserDto): void {
        abp.message.confirm(
            "Delete user '" + user.fullName + "'?",
            (result: boolean) => {
                if (result) {
                    this._userService.delete(user.id)
                        .subscribe(() => {
                            abp.notify.info("Deleted User: " + user.fullName);
                            this.refresh();
                        });
                }
            }
        );
    }

    // Show Modals
    createUser(): void {
        this.createUserModal.show();
    }

    editUser(user: UserDto): void {
        this.editUserModal.show(user.id);
    }
}
