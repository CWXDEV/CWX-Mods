import { DatabaseServer } from "@spt-aki/servers/DatabaseServer";
import { globalsConfig } from "models/IConfig";

import { inject, injectable } from "tsyringe";
import { CWX_ConfigHandler } from "./configHandler";

@injectable()
export class CWX_GlobalsConfig
{
    private tables;
    private config: globalsConfig;
    
    constructor(
        @inject("DatabaseServer") private databaseServer: DatabaseServer,
        @inject("CWX_ConfigHandler") private configHandler: CWX_ConfigHandler
    )
    {}

    public applyChanges(): void
    {
        this.config = this.configHandler.getConfig().globalsConfig;
        this.tables = this.databaseServer.getTables().globals;

        this.NoFallDamage();
        this.OpenFlea();
        this.QuickScav();
    }

    private NoFallDamage(): void
    {
        if (this.config.noFallDamage)
        {
            this.tables.config.Health.Falling.DamagePerMeter = 0;
            this.tables.config.Health.Falling.SafeHeight = 900;
        }
    }

    private OpenFlea(): void 
    {
        if (this.config.openFlea)
        {
            this.tables.config.RagFair.minUserLevel = 1;
        }
    }

    private QuickScav(): void
    {
        if (this.config.quickScav)
        {
            this.tables.config.SavagePlayCooldown = 1;
        }
    }
}