# Kedar Patki
# IMGS 624 Virtual Environments Project
# Detect faces and generate JSON containing bounding box information 

import numpy as np
import cv2
#import pdb
import json
import pprint

#------------------------------------------------------------------------------

def convert_equirect_to_spherical( x, y, width, height, radius = 50 ):
    # According to Unity conventions
    theta = -np.pi + ( ( 2 * np.pi ) / width ) * x
    phi = ( np.pi / 2 ) + ( -np.pi / height ) * y
    
    X = radius * np.cos( phi ) * np.cos( theta )
    Z = radius * np.cos( phi ) * np.sin( theta )
    Y = radius * np.sin( phi )
    
    return [X,Y,Z]



#------------------------------------------------------------------------------

if __name__ == '__main__':

    # Load the cascade
    # Credits: https://towardsdatascience.com/face-detection-in-2-minutes-using-opencv-python-90f89d7c0f81
    face_cascade = cv2.CascadeClassifier('haarcascade_frontalface_default.xml')

    # Load the video source
    cap = cv2.VideoCapture('/home/kpatki/unity3d_projects/VR-360/Assets/Videos/london_on_tower_bridge.ogv')
    # cap = cv2.VideoCapture('/home/kpatki/unity3d_projects/VR-360/FaceDetection/test.ogv')
    # cap = cv2.VideoCapture('/home/kpatki/unity3d_projects/VR-360/FaceDetection/test2.ogv')

    v = 0
    n_frames = 800 # based on 29 secs x 30 fps = 870 frames in the video # test 150 frames, conservative 
    #pdb.set_trace()

    bboxes_dict = { 'frames' : [], 'numframes' : n_frames }

    while v < n_frames:
        
        print( 'frame {}/{}'.format( v, n_frames ) )
        
        # Read the frame
        _, img = cap.read()

        if v == 0: print( img.shape )
        
        # Add a new frame to the dictionary
        # if v < n_frames:
        bboxes_dict['frames'].append( { 'bboxes' : [] } )

        # Convert to grayscale
        gray = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)

        # Detect the faces
        faces = face_cascade.detectMultiScale(gray, 1.1, 4)
        
        # Draw the rectangle around each face and save information to dictionary
        for (x, y, w, h) in faces:
            # cv2.rectangle(img, (x, y), (x+w, y+h), (255, 0, 0), 2)

            bboxes_dict['frames'][-1]['bboxes'].append({
                'top_left' : convert_equirect_to_spherical( x, y, img.shape[1], img.shape[0] ),
                'top_right' : convert_equirect_to_spherical( x+w, y, img.shape[1], img.shape[0] ),
                'bottom_left' : convert_equirect_to_spherical( x, y+h, img.shape[1], img.shape[0] ),
                'bottom_right' : convert_equirect_to_spherical( x+w, y+h, img.shape[1], img.shape[0] )
                })
        
        # resize and display
        # resized_img = cv2.resize( img, (800,400) )
        # if v == 0: print( resized_img.shape )
        v += 1
        
        # cv2.imshow( 'resized_img', resized_img )
        
        # Stop if escape key is pressed
        # k = cv2.waitKey(30) & 0xff
        # if k==27:
        #     break

    cap.release()

    # Print dictionary to json file
    pp = pprint.PrettyPrinter( indent = 4 )
    pp.pprint( bboxes_dict )
    with open('/home/kpatki/.config/unity3d/DefaultCompany/VR-360/data.json', 'w') as f:
        json.dump(bboxes_dict, f)