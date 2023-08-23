export const getMenuItem = (key,label, component,hoverColor, children, type)=>{

    return {
            key,
            icon:component,
            children,
            label,
            type,
            // danger:'danger',
            style:{
              '&:hover':{
                color:'black'
              },
            }
          };
        }