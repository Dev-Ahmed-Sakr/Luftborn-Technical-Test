import { Routes } from '@angular/router';
import { ProductListComponent } from './components/product-list/product-list.component';
import { ProductFormComponent } from './components/product-form/product-form.component';

export const routes: Routes = [
  { path: '', redirectTo: '/products', pathMatch: 'full' },
  { path: 'products', component: ProductListComponent },
  { path: 'products/add', loadComponent: () => import('./components/product-form/product-form.component').then(m => m.ProductFormComponent) },
  { path: 'products/edit/:id', loadComponent: () => import('./components/product-form/product-form.component').then(m => m.ProductFormComponent) },
  { path: '**', redirectTo: '/products' }
];
