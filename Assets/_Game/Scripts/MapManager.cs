using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Aezakmi
{
    public class MapManager : GloballyAccessibleBase<MapManager>
    {
        [SerializeField] private Camera mapCamera;
        [SerializeField] private float rotateMapSpeed;
        [SerializeField] private Transform mapSphere;
        [SerializeField] private LayerMask mapSphereLayer;
        [SerializeField] private GameObject rotateArea;
        [SerializeField] private GameObject mapImage;
        [SerializeField] private RectTransform mapRectTr;
        [SerializeField] private RectTransform canvasRectTransform;

        [Header("Popup")]
        [SerializeField] private MapPopupUI popUp;

        public RectTransform img;

        private Vector2? m_startPosition = null;
        private float m_targetSpeed = 0;
        private int m_direction = 0;

        private bool m_hitRotateArea = false;

        // Map Settings
        private bool m_hitMapImage = false;
        private Vector2 m_mapPosition;
        private Vector2 m_mapSize = new Vector2(1000, 1000);

        private const int LERP_SPEED = 6;

        private Region? m_selectedRegion;

        private void Start()
        {
            m_mapPosition = mapImage.transform.position;
        }

        private void Update()
        {
            if (InputManager.Instance.IsTouching)
            {
                // Raycast to check what was hit.
                m_hitRotateArea = false;
                m_hitMapImage = false;
                PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
                eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(eventDataCurrentPosition, results);


                foreach (var hitItem in results)
                {
                    if (hitItem.gameObject == rotateArea) m_hitRotateArea = true;
                    if (hitItem.gameObject == mapImage && Input.GetMouseButtonDown(0) && !popUp.gameObject.activeSelf)
                    {
                        var screenPos = hitItem.screenPosition;
                        var canvasSize = canvasRectTransform.sizeDelta;
                        Vector2 localPoint;
                        RectTransformUtility.ScreenPointToLocalPointInRectangle(mapRectTr, screenPos, null, out localPoint);

                        Vector2 vectorNormalized = (localPoint / m_mapSize) * 2f;
                        SelectMap(vectorNormalized);
                    }
                }
            }

            RotatePlanet();
        }

        private void RotatePlanet()
        {
            if (!InputManager.Instance.IsTouching) m_targetSpeed = Mathf.Lerp(m_targetSpeed, 0f, LERP_SPEED * Time.deltaTime);

            if (Input.GetMouseButtonDown(0))
                m_startPosition = Input.mousePosition;
            else if (Input.GetMouseButtonUp(0))
            {
                m_startPosition = null;
                m_targetSpeed = Mathf.Lerp(m_targetSpeed, 0f, LERP_SPEED * Time.deltaTime);
            }

            if (m_startPosition != null && m_hitRotateArea)
            {
                m_direction = m_startPosition.Value.x >= Input.mousePosition.x ? 1 : -1;
                m_targetSpeed = Mathf.Lerp(m_targetSpeed, rotateMapSpeed, LERP_SPEED * Time.deltaTime);
            }
            else if (!m_hitRotateArea)
                m_targetSpeed = Mathf.Lerp(m_targetSpeed, 0f, LERP_SPEED * Time.deltaTime);

            mapSphere.localEulerAngles += m_direction * m_targetSpeed * Vector3.up * Time.deltaTime;
        }
        private void SelectMap(Vector2 pos)
        {
            RaycastHit hit;
            Vector3 startPosition = (mapCamera.transform.position + (Vector3)pos * mapCamera.orthographicSize);
            Debug.DrawRay(startPosition, Vector3.forward * 5, Color.red, 5f);
            if (Physics.Raycast(startPosition, Vector3.forward, out hit, 100f, mapSphereLayer))
            {
                PopupMapSelection(hit.transform.gameObject.GetComponent<MapSelection>().region);
            }
        }

        private void PopupMapSelection(Region region)
        {
            if (region == Region.None) return;

            m_selectedRegion = region;
            popUp.gameObject.SetActive(true);
            popUp.SetRegion(region);
        }

        public void ConfirmRegion()
        {

        }

        public void ExitMapZone()
        {
            mapCamera.gameObject.SetActive(false);
            canvasRectTransform.gameObject.SetActive(false);
        }

        public void EnterMapZone()
        {
            mapCamera.gameObject.SetActive(true);
            canvasRectTransform.gameObject.SetActive(true);
        }
    }
}
