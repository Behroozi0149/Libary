import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.component';


bootstrapApplication(AppComponent, appConfig)
  .catch((err) => console.error(err));

// change title
const titleTag = document.title;
window.addEventListener("blur", function () {
  document.title = "Library management | Waiting💙";
});
window.addEventListener("focus", function () {
  document.title = titleTag;
});

