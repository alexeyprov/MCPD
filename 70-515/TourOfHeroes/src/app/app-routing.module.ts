import { ModuleWithProviders } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import { DashboardComponent } from "./dashboard.component";
import { HeroDetailComponent } from "./hero-detail.component";
import { HeroesComponent } from "./heroes.component";

const AppRoutes: Routes = [
    {
        path: "heroes",
        component: HeroesComponent
    },
    {
        path: "dashboard",
        component: DashboardComponent
    },
    {
        path: "",
        pathMatch: "full",
        redirectTo: "dashboard"
    },
    {
        path: "detail/:id",
        component: HeroDetailComponent
    }
];

export const AppRoutingModule: ModuleWithProviders = RouterModule.forRoot(AppRoutes);