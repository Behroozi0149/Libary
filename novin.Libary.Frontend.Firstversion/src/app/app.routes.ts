import { Routes } from '@angular/router';
import { BooksComponent } from './+pages/books/books.component';
import { BorrowsComponent } from './+pages/borrows/borrows.component';
import { MembersComponent } from './+pages/members/members.component';
import { LoginComponent } from './+pages/login/login.component';

export const routes: Routes = [
    {path:'books',component:BooksComponent},
    {path:'members',component:MembersComponent},
    {path:'borrows',component:BorrowsComponent},
    {path:'login',component:LoginComponent},
    {path:'',redirectTo:'/login',pathMatch:'full'},
    {path:'**',redirectTo:'/login'}
];
