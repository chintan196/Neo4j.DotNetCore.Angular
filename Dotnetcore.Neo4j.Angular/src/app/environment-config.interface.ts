import { InjectionToken } from "@angular/core";

// environment-config.interface.ts
/**
 * Environment configinterface - this is to fetch environment specific settings
 */
export interface EnvironmentConfig {
  environment: {
    baseUrl: string;
    bloomUrl: string;
  };
}

export const ENV_CONFIG = new InjectionToken<EnvironmentConfig>('EnvironmentConfig');
