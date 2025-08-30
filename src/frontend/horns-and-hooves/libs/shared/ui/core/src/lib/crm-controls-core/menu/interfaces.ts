export interface IMenuItem {
    label?: string;
    link?: any;
    routerLink?: any;
    icon?: string;
    children?: IMenuItem[];
    roles?: string[];
    claims?: string[];
    routerLinkActiveOptions?: any;
    separator?: boolean;
    disabled?: boolean;
    visible?: boolean;
    class?: string;
    badge?: string;
    badgeStyleClass?: string;
    command?: (event?: any) => void;
    items?: IMenuItem[];
}


export interface IMenuProvider {
    key: string;
    getMenuItems(): IMenuItem[]
}