import { Routes } from '@angular/router';
import { BooksComponent } from './+pages/books/books.component';
import { BorrowsComponent } from './+pages/borrows/borrows.component';
import { MembersComponent } from './+pages/members/members.component';

export const routes: Routes = [
    {path:'books',component:BooksComponent},
    {path:'members',component:MembersComponent},
    {path:'borrows',component:BorrowsComponent},
    {path:'',redirectTo:'/books',pathMatch:'full'},
    {path:'**',redirectTo:'/books'}
];
