import { InjectionToken } from "@angular/core";

// environment-config.interface.ts
export interface EnvironmentConfig {
  environment: {
    baseUrl: string;
  };
}

export const ENV_CONFIG = new InjectionToken<EnvironmentConfig>('EnvironmentConfig');
