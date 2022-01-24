from ast import Try
import flask
from flask import request
import base64
import cv2
import numpy as np
import ocr
app = flask.Flask(__name__)
app.config["DEBUG"] = False


@app.route('/', methods=['Get'])
def home():
    return "<h1>Hallo</p>"

@app.route('/getcardname', methods=['POST'])
def create_task():
    jsonstr = request.get_json()
    img = jsonstr["img"]
    im_bytes = base64.b64decode(img)
    print(im_bytes)
    im_arr = np.frombuffer(im_bytes, dtype=np.uint8)  # im_arr is one-dim Numpy array
    img = cv2.imdecode(im_arr, flags=cv2.IMREAD_COLOR)
    try:
        card_name = ocr.gettext(img)
    except:
        card_name = ""
    return card_name

app.run(host="0.0.0.0")
