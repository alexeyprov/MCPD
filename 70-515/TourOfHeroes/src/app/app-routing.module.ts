import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import { DashboardComponent } from "./dashboard/dashboard.component";
import { HeroDetailComponent } from "./hero-detail/hero-detail.component";
import { HeroesComponent } from "./heroes/heroes.component";

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
        redirectTo: "/dashboard"
	},
    {
		path: "detail/:id",
		component: HeroDetailComponent 
	}
];

@NgModule({
  imports: [
    RouterModule.forRoot(AppRoutes)
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { 
}