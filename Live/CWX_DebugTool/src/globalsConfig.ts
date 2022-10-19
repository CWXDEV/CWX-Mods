import { IGlobals } from "@spt-aki/models/eft/common/IGlobals";
import { DatabaseServer } from "@spt-aki/servers/DatabaseServer";
import { GlobalsConfig } from "models/IConfig";

import { inject, injectable } from "tsyringe";
import { CwxConfigHandler } from "./configHandler";

@injectable()
export class CwxGlobalsConfig
{
    private tables: IGlobals;
    private config: GlobalsConfig;
    
    constructor(
        @inject("DatabaseServer") private databaseServer: DatabaseServer,
        @inject("CwxConfigHandler") private configHandler: CwxConfigHandler
    )
    {}

    public applyChanges(): void
    {
        this.config = this.configHandler.getConfig().globalsConfig;
        this.tables = this.databaseServer.getTables().globals;

        this.noFallDamage();
        this.openFlea();
        this.quickScav();
    }

    private noFallDamage(): void
    {
        if (this.config.noFallDamage)
        {
            this.tables.config.Health.Falling.DamagePerMeter = 0;
            this.tables.config.Health.Falling.SafeHeight = 900;
        }
    }

    private openFlea(): void 
    {
        if (this.config.openFlea)
        {
            this.tables.config.RagFair.minUserLevel = 1;
        }
    }

    private quickScav(): void
    {
        if (this.config.quickScav)
        {
            this.tables.config.SavagePlayCooldown = 1;
        }
    }
}