import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.component';

bootstrapApplication(AppComponent, appConfig)
  .catch((err) => console.error(err));

console.log('WELCOME');
console.log('SUPPORT:tahabehroozi0149@gmail.com');
console.log('INSTAGRAM:taha_behroozi_');
console.log('TELEGRAM:dev_taha_behroozi');
// change title
const titleTag = document.title;
window.addEventListener("blur", function () {
  document.title = "Library management | Waiting";
});
window.addEventListener("focus", function () {
  document.title = titleTag;
});
