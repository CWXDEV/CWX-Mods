import { injectable } from "tsyringe";
import { ICwxConfig } from "../models/IConfig";

@injectable()
export class CwxConfigHandler
{
    private config: ICwxConfig;

    constructor()
    {
        this.config = require("../config/config.json");
    }

    public getConfig(): ICwxConfig
    {
        return this.config;
    }
}