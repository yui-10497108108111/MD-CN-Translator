import easyocr
import re
reader = easyocr.Reader(['en']) 
def gettext(img):
    text = reader.readtext(img,detail=0)[0]
    return re.compile('[^A-Z^a-z^0-9^ ]').sub('',text)