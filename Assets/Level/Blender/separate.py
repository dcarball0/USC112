import bpy

def main():
    origin_obj = bpy.context.active_object
    origin_name = origin_obj.name
    keys = origin_obj.vertex_groups.keys()
    real_keys = []

    bpy.ops.object.mode_set(mode='EDIT')

    for gr in keys:
        bpy.ops.object.vertex_group_set_active(group=gr)
        bpy.ops.mesh.select_all(action='DESELECT')
        bpy.ops.object.vertex_group_select()
        
        if bpy.context.selected_objects:
            try:
                bpy.ops.mesh.separate(type='SELECTED')
                real_keys.append(gr)
            except:
                pass

    bpy.ops.object.mode_set(mode='OBJECT')
    
    for i, obj in enumerate(bpy.context.selected_objects, start=1):
        if obj.name.startswith(origin_name):
            new_name = f'{origin_name}.{real_keys[i - 1]}'
            obj.name = new_name

if __name__ == '__main__':
    main()
