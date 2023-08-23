# test with 1 static example - 2 objects only - above
preposition_name="above"
root_folder_path="D:/SPUR NEURIPS"
preposition_folder_path=os.path.join(root_folder_path,preposition_name)

#create an empty df
columns=[
    "preposition",
    "category",
    "image_file_name",
    "object_1_information",
    "object_2_information",
    "object_1_2d_bounding_boxes",
    "object_2_2d_bounding_boxes",
    "object_1_3d_bounding_boxes",
    "object_2_3d_bounding_boxes",
]
 
df=pd.DataFrame(columns=columns)

categories=os.listdir(preposition_folder_path)
for category in tqdm(categories):
    category_folder_path=os.path.join(preposition_folder_path,category)
    category_folder_path_list=os.listdir(category_folder_path)
    category_folder_path_list=sorted(category_folder_path_list)
    dataset_folder_path=os.path.join(category_folder_path,category_folder_path_list[0])
    dataset_folder_path_list=os.listdir(dataset_folder_path)
    captures_list=[x for x in dataset_folder_path_list if x.startswith("captures")]
    for captures_json_file in captures_list:
        full_json_path=os.path.join(dataset_folder_path,captures_json_file)
        with open(full_json_path) as file:
            json_data = json.load(file)
            for data in json_data["captures"]:
                filename=data["filename"].split("/")[-1]
                annotations=data["annotations"]
                if annotations[0]["id"]=="bounding box 3D":
                    info_3d=annotations[0]
                    info_2d=annotations[1]
                else:
                    info_3d=annotations[1]
                    info_2d=annotations[0]

                try:
                    object1_info_dict=info_2d["values"][0]
                    object2_info_dict=info_2d["values"][1]
                    object1_info=object1_info_dict["label_name"]
                    object2_info=object2_info_dict["label_name"]

                    object1_bb_2d=[object1_info_dict["x"],object1_info_dict["y"],object1_info_dict["width"],object1_info_dict["height"]]
                    object2_bb_2d=[object2_info_dict["x"],object2_info_dict["y"],object2_info_dict["width"],object2_info_dict["height"]]

                    object1_info_dict=info_3d["values"][0]
                    object2_info_dict=info_3d["values"][1]
                    object1_bb_3d=object1_info_dict["size"]
                    object2_bb_3d=object2_info_dict["size"]
                except:
                    object1_info=""
                    object2_info=""
                    object1_bb_2d=[]
                    object2_bb_2d=[]
                    object1_bb_3d=[]
                    object2_bb_3d=[]
#                     print(f"error for {preposition_name}, {category}, {filename}, df index: {len(df)} ")

                new_row={
                    "preposition":preposition_name,
                    "category":category,
                    "image_file_name":filename,
                    "object_1_information":object1_info,
                    "object_2_information":object2_info,
                    "object_1_2d_bounding_boxes":object1_bb_2d,
                    "object_2_2d_bounding_boxes":object2_bb_2d,
                    "object_1_3d_bounding_boxes":object1_bb_3d,
                    "object_2_3d_bounding_boxes":object2_bb_3d,    
                }
                df.loc[len(df)] = new_row

df