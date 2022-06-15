import { CommonModule } from "@angular/common";
import { ModuleWithProviders, NgModule } from "@angular/core";
import { EnvironmentConfig, ENV_CONFIG } from "./environment-config.interface";

/**
 * Ng module - HTTP module for api calls
 */
@NgModule({
  imports: [CommonModule]
})
export class HttpModule {
  static forRoot(config: EnvironmentConfig): ModuleWithProviders<HttpModule> {
    return {
      ngModule: HttpModule,
      providers: [
        {
          provide: ENV_CONFIG,
          useValue: config
        }
      ]
    };
  }
}
