import { Component, Injector, ViewEncapsulation } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { MenuItem } from '@shared/layout/menu-item';

@Component({
    templateUrl: './sidebar-nav.component.html',
    selector: 'sidebar-nav',
    encapsulation: ViewEncapsulation.None
})
export class SideBarNavComponent extends AppComponentBase {

    menuItems: MenuItem[] = [
        new MenuItem(this.l("HomePage"), "", "home", "/app/home"),

        new MenuItem(this.l("Users"), "Pages.Users", "people", "/app/users"),
        new MenuItem("Department", "Pages.Users", "business", "/app/department"),
        new MenuItem("Transaction", "Pages.Users", "local_offer", "/app/transaction"),
        new MenuItem(this.l("About"), "", "info", "/app/about") ,      
        new MenuItem(this.l("Roles"), "Pages.Roles", "R", "/app/roles"),
        new MenuItem(this.l("Tenants"), "Pages.Tenants", "T", "/app/tenants")       
                                                                        
    ];
                

    constructor(
        injector: Injector
    ) {
        super(injector);
    }

    showMenuItem(menuItem): boolean {
        if (menuItem.permissionName) {
            return this.permission.isGranted(menuItem.permissionName);
        }

        return true;
    }
}
